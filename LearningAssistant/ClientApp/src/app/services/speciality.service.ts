import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Speciality } from '../classes/iismodels';

@Injectable({
  providedIn: 'root'
})
export class SpecialitiesService {

  private url = "/api/specialityes";

  constructor(private http: HttpClient) { }

  getSpecialitites() {
    return this.http.get(this.url);
  }

}
