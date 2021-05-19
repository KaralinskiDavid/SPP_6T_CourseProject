import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Group } from '../classes/iismodels';

@Injectable({
  providedIn: 'root'
})
export class GroupService {

  private url = "/api/groups";

  constructor(private http: HttpClient) { }

  getGroups() {
    return this.http.get(this.url);
  }

  checkGroupNumber(groupNumber: string) {
    return this.http.get(this.url + "/check/" + groupNumber);
  }

  getGroupByNumber(groupNumber: string) {
    return this.http.get(this.url + "/" + groupNumber);
  }

}
