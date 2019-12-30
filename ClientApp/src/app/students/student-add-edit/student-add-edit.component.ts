import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";

import { StudentService } from "src/app/services/student.service";
import { Student } from "src/app/models/Student";

@Component({
  selector: "app-student-add-edit",
  templateUrl: "./student-add-edit.component.html",
  styleUrls: ["./student-add-edit.component.css"]
})
export class StudentAddEditComponent implements OnInit {
  studentForm: FormGroup;
  submitted = false;
  actionType = "Add";
  bloodGroups = ["A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-"];
  student;
  studentId: number;

  constructor(
    private formBuilder: FormBuilder,
    private studentService: StudentService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    if (this.route.snapshot.params["id"]) {
      this.studentId = this.route.snapshot.params["id"];
    }
    console.log("studentId", this.studentId);

    this.studentForm = this.formBuilder.group({
      Id: 0,
      Name: ["", Validators.required],
      Email: [""],
      Phone: [""],
      Gender: ["", Validators.required],
      BloodGroup: ["", Validators.required],
      Address: this.formBuilder.group({
        Street: ["", Validators.required],
        City: ["", Validators.required],
        State: [""],
        Country: ["", Validators.required]
      })
    });
  }

  async ngOnInit() {
    try {
      if (this.studentId > 0) {
        this.actionType = "Edit";

        this.student = await this.studentService.getStudent(this.studentId);

        this.id.setValue(this.student.id);
        this.name.setValue(this.student.name);
        this.email.setValue(this.student.email);
        this.phone.setValue(this.student.phone);
        this.gender.setValue(this.student.gender);
        this.bloodGroup.setValue(this.student.bloodGroup);
        this.street.setValue(this.student.address.street);
        this.city.setValue(this.student.address.city);
        this.state.setValue(this.student.address.state);
        this.country.setValue(this.student.address.country);
      }
    } catch (error) {
      console.error(error);
    }
  }

  get id() {
    return this.studentForm.get("Id");
  }
  get name() {
    return this.studentForm.get("Name");
  }
  get email() {
    return this.studentForm.get("Email");
  }
  get phone() {
    return this.studentForm.get("Phone");
  }
  get gender() {
    return this.studentForm.get("Gender");
  }
  get bloodGroup() {
    return this.studentForm.get("BloodGroup");
  }
  get street() {
    return this.studentForm.get("Address").get("Street");
  }
  get city() {
    return this.studentForm.get("Address").get("City");
  }
  get state() {
    return this.studentForm.get("Address").get("State");
  }
  get country() {
    return this.studentForm.get("Address").get("Country");
  }

  async onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.studentForm.invalid) {
      return;
    }

    try {
      if (this.studentId) {
        let updateStudent: Student = {
          Id: this.id.value,
          Name: this.name.value,
          Email: this.email.value,
          Phone: this.phone.value,
          Gender: this.gender.value,
          BloodGroup: this.bloodGroup.value,
          Address: {
            Street: this.street.value,
            City: this.city.value,
            State: this.state.value,
            Country: this.country.value
          }
        };

        await this.studentService.updateStudent(this.studentId, updateStudent);

        console.log("Successfully updated student");
      } else {
        let newStudent: Student = {
          Name: this.name.value,
          Email: this.email.value,
          Phone: this.phone.value,
          Gender: this.gender.value,
          BloodGroup: this.bloodGroup.value,
          Address: {
            Street: this.street.value,
            City: this.city.value,
            State: this.state.value,
            Country: this.country.value
          }
        };

        await this.studentService.createStudent(newStudent);

        console.log("Successfully created student");
      }

      this.router.navigate(["/students"]);
    } catch (error) {
      console.error(error);
    }
  }

  onReset() {
    this.router.navigate(["/students"]);
  }
}
