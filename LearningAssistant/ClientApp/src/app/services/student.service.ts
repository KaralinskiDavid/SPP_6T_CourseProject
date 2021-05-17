import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Student } from '../classes/iismodels';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private url = "/api/students";

  constructor(private http: HttpClient) { }

  getStudents() {
    return this.http.get(this.url);
  }

}
