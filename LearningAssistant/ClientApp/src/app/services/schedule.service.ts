import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Schedule } from '../classes/iismodels';

@Injectable({
  providedIn: 'root'
})
export class ScheduleService {

  private url = "/api/schedule";

  constructor(private http: HttpClient) { }

  getCurrentWeek() {
    return this.http.get(this.url + "/currentWeek");
  }

  getGroupSchedule(groupNumber: string) {
    return this.http.post(this.url + "/" + groupNumber, null);
  }

  refreshGroupSchedule(groupNumber: string) {
    return this.http.post(this.url + "/refresh/" + groupNumber, null);
  }

}
