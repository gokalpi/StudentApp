import { Component, OnInit, AfterViewInit, OnDestroy, ElementRef, ViewChildren } from "@angular/core";
import { FormControlName, FormGroup, FormBuilder, Validators } from "@angular/forms";
import { Subscription, Observable, fromEvent, merge } from "rxjs";
import { Router, ActivatedRoute } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { debounceTime } from "rxjs/operators";

import { StudentService } from "src/app/services/student.service";
import { Student } from "src/app/models/Student";
import { GenericValidator } from "src/app/shared/generic-validator";

@Component({
  selector: "app-student-add-edit",
  templateUrl: "./student-add-edit.component.html",
  styleUrls: ["./student-add-edit.component.css"]
})
export class StudentAddEditComponent implements OnInit, AfterViewInit, OnDestroy {
  bloodGroups = ["A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-"];
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements: ElementRef[];
  pageTitle = "Student Edit";
  studentForm: FormGroup;
  student: Student;
  private sub: Subscription;
  errorMessage: string;

  displayMessage: { [key: string]: string } = {};
  private validationMessages: { [key: string]: { [key: string]: string } };
  private genericValidator: GenericValidator;

  constructor(
    private formBuilder: FormBuilder,
    private studentService: StudentService,
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService
  ) {
    this.validationMessages = {
      name: {
        required: "Student name is required.",
        minlength: "Student name must be at least two characters.",
        maxlength: "Student name cannot exceed 50 characters."
      },
      gender: {
        required: "Student gender is required."
      },
      bloodGroup: {
        required: "Student blood group is required."
      },
      street: {
        required: "Student street is required.",
        minlength: "Student street must be at least two characters.",
        maxlength: "Student street cannot exceed 100 characters."
      },
      city: {
        required: "Student city is required.",
        minlength: "Student city must be at least two characters.",
        maxlength: "Student city cannot exceed 100 characters."
      },
      country: {
        required: "Student country is required.",
        minlength: "Student country must be at least two characters.",
        maxlength: "Student country cannot exceed 100 characters."
      }
    };

    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit() {
    this.studentForm = this.formBuilder.group({
      id: 0,
      name: [
        "",
        Validators.required
      ],
      email: [""],
      phone: [""],
      gender: ["", Validators.required],
      bloodGroup: ["", Validators.required],
      address: this.formBuilder.group({
        street: [
          "",
          Validators.required
        ],
        city: [
          "",
          Validators.required
        ],
        state: [""],
        country: [
          "",
          Validators.required
        ]
      })
    });

    this.sub = this.route.paramMap.subscribe(params => {
      const id = parseInt(params.get("id"));
      if (isNaN(id)) {
        console.log("Adding new student");

        const newStudent = {
          id: 0,
          name: "",
          email: "",
          phone: "",
          gender: "",
          bloodGroup: "",
          address: {
            street: "",
            city: "",
            state: "",
            country: ""
          }
        };

        this.displayStudent(newStudent);
      } else {
        console.log(`Updating student ${id}`);
        this.getStudent(id);
      }
    });
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  ngAfterViewInit(): void {
    // Watch for the blur event from any input element on the form.
    const controlBlurs: Observable<any>[] = this.formInputElements.map((formControl: ElementRef) =>
      fromEvent(formControl.nativeElement, "blur")
    );

    // Merge the blur event observable with the valueChanges observable
    merge(this.studentForm.valueChanges, ...controlBlurs)
      .pipe(debounceTime(800))
      .subscribe(value => {
        this.displayMessage = this.genericValidator.processMessages(
          this.studentForm
        );
      });
  }

  getStudent(id: number) {
    this.studentService.getStudent(id).subscribe(
      response => {
        this.student = <Student>response.result;
        this.displayStudent(this.student);
      },
      error => (this.errorMessage = <any>error)
    );
  }

  displayStudent(student: Student): void {
    if (this.studentForm) {
      this.studentForm.reset();
    }

    if (student.id == 0) {
      this.pageTitle = "Add Student";
    } else {
      this.pageTitle = `Edit Student: ${this.student.name}`;
    }

    // Update the data on the form
    this.studentForm.patchValue({
      id: student.id,
      name: student.name,
      email: student.email,
      phone: student.phone,
      gender: student.gender,
      bloodGroup: student.bloodGroup,
      address: {
        street: student.address.street,
        city: student.address.city,
        state: student.address.state,
        country: student.address.country
      }
    });
  }

  saveStudent(): void {
    if (this.studentForm.valid) {
      if (this.studentForm.dirty) {
        const p = { ...this.student, ...this.studentForm.value };
        if (p.id === 0) {
          this.studentService.createStudent(p).subscribe(
            () => {
              this.toastr.success(`Created student ${p.name}`, "Success", {
                timeOut: 2000
              });

              this.onSaveComplete();
            },
            (error: any) => {
              this.toastr.error(`Error creating student ${error}`, "Error", {
                timeOut: 2000
              });

              this.errorMessage = <any>error;
            }
          );
        } else {
          this.studentService.updateStudent(p).subscribe(
            () => {
              this.toastr.success(`Updated student ${p.name}`, "Success", {
                timeOut: 2000
              });

              this.onSaveComplete();
            },
            (error: any) => {
              this.toastr.error(`Error updating student ${error}`, "Error", {
                timeOut: 2000
              });

              this.errorMessage = <any>error;
            }
          );
        }
      } else {
        this.onSaveComplete();
      }
    } else {
      this.errorMessage = "Please correct the validation errors.";
    }
  }

  onSaveComplete(): void {
    // Reset the form to clear the flags
    this.studentForm.reset();
    this.router.navigate(["/students"]);
  }
}
