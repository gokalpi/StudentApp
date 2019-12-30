import { Injectable } from "@angular/core";
import Axios from "axios";

import { apiEndpoint } from "src/config";
import { Student } from "src/app/models/Student";

@Injectable({
  providedIn: "root"
})
export class StudentService {
  async getAllStudents(): Promise<Student[]> {
    console.log("Getting all students");

    const response = await Axios.get(`${apiEndpoint}/students`, {
      headers: {
        "Content-Type": "application/json"
      }
    });

    // console.log("Students:", response.data);
    return response.data;
  }

  async getStudent(studentId: number): Promise<Student> {
    console.log(`Getting student ${studentId}`);

    const response = await Axios.get(`${apiEndpoint}/students/${studentId}`, {
      headers: {
        "Content-Type": "application/json"
      }
    });

    // console.log("Student:", response.data);
    return response.data;
  }

  async createStudent(newStudent: any): Promise<Student> {
    console.log("Creating student");

    const response = await Axios.post(
      `${apiEndpoint}/students`,
      JSON.stringify(newStudent),
      {
        headers: {
          "Content-Type": "application/json"
        }
      }
    );

    // console.log("Created student:", response.data);
    return response.data;
  }

  async updateStudent(studentId: number, updatedStudent: any): Promise<void> {
    console.log(`Updating student ${studentId}`);

    await Axios.put(
      `${apiEndpoint}/students/${studentId}`,
      JSON.stringify(updatedStudent),
      {
        headers: {
          "Content-Type": "application/json"
        }
      }
    );
  }

  async deleteStudent(studentId: number): Promise<void> {
    console.log(`Deleting student ${studentId}`);

    await Axios.delete(`${apiEndpoint}/students/${studentId}`, {
      headers: {
        "Content-Type": "application/json"
      }
    });
  }
}
