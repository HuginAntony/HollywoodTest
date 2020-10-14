import { Tournament } from 'src/app/shared/models/tournament.model';
import { Event } from 'src/app/shared/models/event.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';



@Injectable({
  providedIn: 'root',
})
export class TournamentService {
  private url = `https://localhost:44355/tournament`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Tournament[]> {
    return this.http.get<Tournament[]>(this.url);
  }

  get(id: number): Observable<Tournament> {
    return this.http.get<Tournament>(`${this.url}/${id}`);
  }

  getEventByTournament(tournamentId: number): Observable<Event[]> {
    return this.http.get<Event[]>(`${this.url}/${tournamentId}/events`);
  }

  isNameValid(name: string): Observable<boolean>{
    return this.http.get<boolean>(`${this.url}/isNameValid/${name}`);
  }

  create(tournament: Tournament): Observable<Tournament> {
    return this.http.post<Tournament>(this.url, JSON.stringify(tournament));
  }

  update(id: number, tournament: Tournament): Observable<Tournament>{
    return this.http.put<Tournament>(`${this.url}/${id}`, JSON.stringify(tournament));
  }

  delete(id: number): Observable<any>{
    return this.http.delete(`${this.url}/${id}`);
  }
}
