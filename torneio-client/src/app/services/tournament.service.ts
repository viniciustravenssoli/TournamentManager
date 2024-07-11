import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Tournament, TournamentToList } from '../Models/models';
import { AuthServiceService } from './auth-service.service';

@Injectable({
  providedIn: 'root'
})
export class TournamentService {
  private apiUrl = 'http://localhost:5135/api/tournaments';

  constructor(private http: HttpClient, private authService: AuthServiceService) { }

  getAllTournaments(): Observable<TournamentToList[]> {
    const token = this.authService.getToken();

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.get<TournamentToList[]>(this.apiUrl, { headers });
  }

  getTournamentById(id: number): Observable<Tournament> {
    return this.http.get<Tournament>(`${this.apiUrl}/${id}`);
  }

  createTournament(tournament: Tournament): Observable<Tournament> {
    const token = this.authService.getToken();

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.post<Tournament>(this.apiUrl, tournament, { headers });
  }

  setMatchWinner(matchId: number, winnerId: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/setwinner`, { matchId, winnerId });
  }
}
