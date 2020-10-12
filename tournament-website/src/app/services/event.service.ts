import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Event } from 'src/app/models/event.model';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  private url = `https://localhost:44355/event`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Event[]> {
    return this.http.get<Event[]>(this.url);
  }

  get(id: string): Observable<Event> {
    return this.http.get<Event>(`${this.url}/${id}`);
  }

  create(event: Event): Observable<Event> {
    return this.http.post<Event>(this.url, JSON.stringify(event));
  }

  update(id: string, event: Event): Observable<Event>{
    return this.http.put<Event>(`${this.url}/${id}`, JSON.stringify(event));
  }

  delete(id: string): Observable<ArrayBuffer>  {
    return this.http.delete(`${this.url}/${id}`, null);
  }
}
