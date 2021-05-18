import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Queue } from '../classes/iismodels';

@Injectable({
  providedIn: 'root'
})
export class QueueService {

  private url = "/api/queues";

  constructor(private http: HttpClient) { }

  createQueue(queue: Queue) {
    return this.http.post(this.url, queue);
  }

  deleteQueue(queueId: number) {
    return this.http.delete(this.url + "/" + queueId);
  }

}
