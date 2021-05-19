import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SpecialityFile, SpecialityFileSection } from '../classes/iismodels';

@Injectable({
  providedIn: 'root'
})
export class FileSectionService {
  private url = "/api/specialityFileSections";


  constructor(private http: HttpClient) { }

  createFileSection(fileSection: SpecialityFileSection) {
    return this.http.post(this.url, fileSection);
  }

  deleteFileSection(id: number) {
    return this.http.delete(this.url + '/' + id);
  }

  getFileSections(specialityId: number) {
    return this.http.get(this.url + '/' + specialityId);
  }
  
}
