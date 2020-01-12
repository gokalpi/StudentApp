import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError, map } from "rxjs/operators";

import { apiEndpoint } from "src/config";
import { Student } from "src/app/models/Student";

@Injectable({
  providedIn: "root"
})
export class StudentService {
  private headers = new HttpHeaders({ "Content-Type": "application/json" });
  private studentsUrl = `${apiEndpoint}/v1/students`;

  constructor(private http: HttpClient) {}

  getStudents(): Observable<Student[]> {
    console.log("Getting all students");

    return this.http
      .get<Student[]>(this.studentsUrl, { headers: this.headers })
      .pipe(catchError(this.handleError));
  }

  getStudent(id: number): Observable<Student> {
    console.log(`Getting student ${id}`);

    return this.http
      .get<Student>(`${this.studentsUrl}/${id}`, { headers: this.headers })
      .pipe(catchError(this.handleError));
  }

  createStudent(student: Student): Observable<Student> {
    console.log("Creating student");

    return this.http
      .post<Student>(this.studentsUrl, student, { headers: this.headers })
      .pipe(catchError(this.handleError));
  }

  updateStudent(student: Student): Observable<Student> {
    console.log(`Updating student ${student.id}`);

    return this.http
      .put<Student>(`${this.studentsUrl}/${student.id}`, student, {
        headers: this.headers
      })
      .pipe(
        map(() => student),
        catchError(this.handleError)
      );
  }

  deleteStudent(id: number): Observable<{}> {
    console.log(`Deleting student ${id}`);

    return this.http
      .delete<Student>(`${this.studentsUrl}/${id}`, { headers: this.headers })
      .pipe(catchError(this.handleError));
  }

  private handleError(err) {
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      errorMessage = `Backend returned code ${err.status}: ${err.body.error}`;
    }
    console.error(err);
    return throwError(errorMessage);
  }
}
