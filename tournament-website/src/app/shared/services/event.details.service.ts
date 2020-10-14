import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EventDetail } from '../models/event-detail.model';

@Injectable({
  providedIn: 'root'
})
export class EventDetailsService {

  private url = `https://localhost:44355/eventDetail`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<EventDetail[]> {
    return this.http.get<EventDetail[]>(this.url);
  }

  get(id: number): Observable<EventDetail> {
    return this.http.get<EventDetail>(`${this.url}/${id}`);
  }

  isNameValid(name: string): Observable<boolean>{
    return this.http.get<boolean>(`${this.url}/isNameValid/${name}`);
  }

  create(eventDetail: EventDetail): Observable<EventDetail> {
    return this.http.post<EventDetail>(this.url, JSON.stringify(eventDetail));
  }

  update(id: number, eventDetail: EventDetail): Observable<EventDetail>{
    return this.http.put<EventDetail>(`${this.url}/${id}`, JSON.stringify(eventDetail));
  }

  delete(id: number): Observable<any>{
    return this.http.delete(`${this.url}/${id}`);
  }

  deleteMany(ids: Array<number>): Observable<any>{
    return this.http.post(`${this.url}/deleteMany`, JSON.stringify(ids));
  }
}
