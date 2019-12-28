import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

@Component({
  selector: "app-student-add-edit",
  templateUrl: "./student-add-edit.component.html",
  styleUrls: ["./student-add-edit.component.css"]
})
export class StudentAddEditComponent implements OnInit {
  studentForm: FormGroup;
  submitted = false;
  actionType = "Add";
  groups = ["A+", "A-", "B+", "B-", "O+", "O-"];

  constructor(private formBuilder: FormBuilder) {
  }

  async ngOnInit() {
    this.studentForm = this.formBuilder.group({
      Name: ["", Validators.required],
      Email: [""],
      Phone: [""],
      Gender: ["", Validators.required],
      BloodGroup: ["", Validators.required],
      Street: ["", Validators.required],
      City: ["", Validators.required],
      State: [""],
      Country: ["", Validators.required]
    });
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.studentForm.controls;
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.studentForm.invalid) {
      return;
    }

    // display form values on success
    alert(
      "SUCCESS!! :-)\n\n" + JSON.stringify(this.studentForm.value, null, 4)
    );
  }

  onReset() {
    this.submitted = false;
    this.studentForm.reset();
  }
}
