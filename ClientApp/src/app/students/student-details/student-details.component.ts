import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";

import { Student } from "src/app/models/Student";
import { StudentService } from "src/app/services/student.service";

@Component({
  selector: "app-student-details",
  templateUrl: "./student-details.component.html",
  styleUrls: ["./student-details.component.css"]
})
export class StudentDetailsComponent implements OnInit {
  student;

  constructor(
    private studentService: StudentService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  async ngOnInit() {
    try {
      var id = parseInt(this.route.snapshot.paramMap.get("id"));
      this.student = await this.studentService.getStudent(id);
      console.log("Student", this.student);
    } catch (error) {
      console.error(error);
    }
  }

  gotoStudentList() {
    this.router.navigate(["/students"]);
  }
}
