import { Component, OnInit } from "@angular/core";
import { ToastrService } from "ngx-toastr";

import { StudentService } from "../../services/student.service";
import { Student } from "src/app/models/Student";

@Component({
  selector: "app-student-list",
  templateUrl: "./student-list.component.html",
  styleUrls: ["./student-list.component.css"]
})
export class StudentListComponent implements OnInit {
  students: any[];

  constructor(
    private studentService: StudentService,
    private toastr: ToastrService
  ) {}

  async ngOnInit() {
    try {
      this.getAllStudents();
    } catch (e) {
      alert(`Failed to get students: ${e.message}`);
    }
  }

  async getAllStudents() {
    try {
      this.students = await this.studentService.getAllStudents();
      console.log("Students", this.students);
    } catch (error) {
      console.error(error);
    }
  }

  public async deleteStudent(studentId: number) {
    const ans = confirm("Do you want to delete student with id: " + studentId);
    if (ans) {
      try {
        await this.studentService.deleteStudent(studentId);
        console.log(`Student with id ${studentId} is deleted`);
        this.toastr.success(`Deleted student ${studentId}`, 'Success', { timeOut: 2000 });

        this.getAllStudents();
      } catch (error) {
        this.toastr.error(`Error deleting student ${studentId}.\nError: ${error}`, 'Error', { timeOut: 2000 });
        console.error(error);
      }
    }
  }
}
