import { EventDetail } from 'src/app/shared/models/event-detail.model';
import { Event } from 'src/app/shared/models/event.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class EventService {

  private url = `https://localhost:44355/event`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Event[]> {
    return this.http.get<Event[]>(this.url);
  }

  get(id: number): Observable<Event> {
    return this.http.get<Event>(`${this.url}/${id}`);
  }

  getEventDetailsByEvent(eventId: number): Observable<EventDetail[]> {
    return this.http.get<EventDetail[]>(`${this.url}/${eventId}/eventDetails`);
  }

  isNameValid(name: string): Observable<boolean>{
    return this.http.get<boolean>(`${this.url}/isNameValid/${name}`);
  }

  create(event: Event): Observable<Event> {
    return this.http.post<Event>(this.url, JSON.stringify(event));
  }

  update(id: number, event: Event): Observable<Event>{
    return this.http.put<Event>(`${this.url}/${id}`, JSON.stringify(event));
  }

  delete(id: number): Observable<any>{
    return this.http.delete(`${this.url}/${id}`);
  }

  deleteMany(ids: Array<number>): Observable<any>{
    return this.http.post(`${this.url}/deleteMany`, JSON.stringify(ids));
  }
}
