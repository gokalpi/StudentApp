import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

import { StudentService } from "../../services/student.service";
import { Student } from "src/app/models/Student";

@Component({
  selector: "app-student-list",
  templateUrl: "./student-list.component.html",
  styleUrls: ["./student-list.component.css"]
})
export class StudentListComponent implements OnInit {
  public students: Student[];

  constructor(private studentService: StudentService, private router: Router) {
    console.log("StudentListComponent constructor");
  }

  async ngOnInit() {
    console.log("ngOnInit method calling..");

    try {
      this.getAllStudents();
    } catch (e) {
      alert(`Failed to get students: ${e.message}`);
    }
  }

  async getAllStudents() {
    this.students = await this.studentService.getAllStudents();
  }

  public async deleteStudent(studentId: number) {
    const ans = confirm("Do you want to delete student with id: " + studentId);
    if (ans) {
      await this.studentService.deleteStudent(studentId);
      this.getAllStudents();
    }
  }
}
