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
  pageTitle = "Student List";
  filteredStudents: any;
  students: any;
  errorMessage = "";
  _listFilter = "";

  get listFilter(): string {
    return this._listFilter;
  }

  set listFilter(value: string) {
    this._listFilter = value;
    this.filteredStudents = this._listFilter
      ? this.performFilter(this._listFilter)
      : this.students;
  }

  constructor(
    private studentService: StudentService,
    private toastr: ToastrService
  ) {}

  performFilter(filterBy: string): Student[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.students.filter(
      (student: Student) =>
        student.name.toLocaleLowerCase().indexOf(filterBy) !== -1
    );
  }

  ngOnInit() {
    this.getStudents();
  }

  getStudents() {
    this.studentService.getStudents().subscribe(
      response => {
        console.log("getStudents Response", response);
        // this.students = response.result;
        // this.filteredStudents = this.students;
      },
      error => (this.errorMessage = <any>error)
    );
  }

  deleteStudent(id: number, name: string): void {
    console.log(`Deleting student ${id} with name ${name}`);

    if (isNaN(id)) {
      // Don't delete, it was never saved.
      this.getStudents();
    } else {
      if (confirm(`Are you sure want to delete this student: ${name}?`)) {
        this.studentService.deleteStudent(id).subscribe(
          () => {
            console.log(`Student ${name} is deleted`);

            this.toastr.success(`Deleted student ${name}`, "Success", {
              timeOut: 2000
            });

            this.getStudents();
          },
          (error: any) => {
            console.error(`Error deleting student ${name}.\nError: ${error}`);

            this.toastr.error(
              `Error deleting student ${name}.\nError: ${error}`,
              "Error",
              { timeOut: 2000 }
            );

            this.errorMessage = <any>error;
          }
        );
      }
    }
  }
}
