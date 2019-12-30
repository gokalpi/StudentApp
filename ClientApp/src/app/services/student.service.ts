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
    return response.data.result;
  }

  async getStudent(studentId: number): Promise<Student> {
    console.log(`Getting student ${studentId}`);

    const response = await Axios.get(`${apiEndpoint}/students/${studentId}`, {
      headers: {
        "Content-Type": "application/json"
      }
    });

    // console.log("Student:", response.data);
    return response.data.result;
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
    return response.data.result;
  }

  async updateStudent(studentId: number, updatedStudent: any): Promise<void> {
    console.log(`Updating student ${studentId}`);

    const response = await Axios.put(
      `${apiEndpoint}/students/${studentId}`,
      JSON.stringify(updatedStudent),
      {
        headers: {
          "Content-Type": "application/json"
        }
      }
    );

    // console.log("Updated student:", response.data);
    return response.data.result;
  }

  async deleteStudent(studentId: number): Promise<void> {
    console.log(`Deleting student ${studentId}`);

    const response = await Axios.delete(`${apiEndpoint}/students/${studentId}`, {
      headers: {
        "Content-Type": "application/json"
      }
    });

    // console.log("Deleted student:", response.data);
    return response.data.result;
  }
}
