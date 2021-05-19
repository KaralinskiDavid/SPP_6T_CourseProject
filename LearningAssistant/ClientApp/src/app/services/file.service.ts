import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SpecialityFile } from '../classes/iismodels';

@Injectable({
  providedIn: 'root'
})
export class FileService {
  private url = "/api/files";


  constructor(private http: HttpClient) { }

  uploadFile(data: FormData, name: string, specialityFileSectionId: number) {
    return this.http.post(this.url + '/?name=' + name + '&specialityFileSectionId=' + specialityFileSectionId, data);
  }

  deleteFile(id) {
    return this.http.delete(this.url + '/' + id);
  }

  downloadFile(id) {
    return this.http.get(this.url + '/' + id, { responseType: 'arraybuffer' });
  }
}
