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
  pageTitle = "Student Detail";
  errorMessage = "";
  student: any;

  constructor(
    private studentService: StudentService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    try {
      const param = this.route.snapshot.paramMap.get("id");
      if (param) {
        const id = parseInt(param);
        this.getStudent(id);
      }
    } catch (error) {
      console.error(error);
    }
  }
  
  getStudent(id: number) {
    this.studentService.getStudent(id).subscribe(
      response => {
        console.log("getStudent Response", response);
        // this.student = response.result;
        // this.displayStudent(this.student);
      },
      error => (this.errorMessage = <any>error)
    );
  }

  onBack(): void {
    this.router.navigate(["/students"]);
  }
}
