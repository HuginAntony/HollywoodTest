import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Tournament } from '../models/tournament.model';

@Injectable({
  providedIn: 'root',
})
export class TournamentService {
  private url = `https://localhost:44355/tournament`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Tournament[]> {
    return this.http.get<Tournament[]>(this.url);
  }

  get(id: string): Observable<Tournament> {
    return this.http.get<Tournament>(`${this.url}/${id}`);
  }

  create(tournament: Tournament): Observable<Tournament> {
    return this.http.post<Tournament>(this.url, JSON.stringify(tournament));
  }

  update(id: string, tournament: Tournament): Observable<Tournament>{
    return this.http.put<Tournament>(`${this.url}/${id}`, JSON.stringify(tournament));
  }

  delete(id: string): Observable<ArrayBuffer>  {
    return this.http.delete(`${this.url}/${id}`, null);
  }
}
