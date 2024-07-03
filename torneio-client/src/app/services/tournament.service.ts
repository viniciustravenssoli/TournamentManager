import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Tournament, TournamentToList } from '../Models/models';

@Injectable({
  providedIn: 'root'
})
export class TournamentService {
  private apiUrl = 'http://localhost:5135/api/tournaments';

  constructor(private http: HttpClient) {}

  getAllTournaments(): Observable<TournamentToList[]> {
    return this.http.get<TournamentToList[]>(this.apiUrl);
  }

  getTournamentById(id: number): Observable<Tournament> {
    return this.http.get<Tournament>(`${this.apiUrl}/${id}`);
  }

  createTournament(tournament: Tournament): Observable<Tournament> {
    return this.http.post<Tournament>(this.apiUrl, tournament);
  }

  setMatchWinner(matchId: number, winnerId: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/setwinner`, { matchId, winnerId });
  }
}
