import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Participant, Tournament } from 'src/app/Models/models';
import { TournamentService } from 'src/app/services/tournament.service';

@Component({
  selector: 'app-create-tournament',
  templateUrl: './create-tournament.component.html',
  styleUrls: ['./create-tournament.component.css']
})
export class CreateTournamentComponent {
  tournament: Tournament = { tournamentId: 0, TournamentName: '', participants: [], matches: [] };
  participants: Participant[] = [{ participantId: 0, name: '' }];

  constructor(private tournamentService: TournamentService, private router: Router) { }

  addParticipant(): void {
    this.participants.push({ participantId: 0, name: '' });
  }

  createTournament(): void {
    this.tournament.participants = this.participants;
    this.tournamentService.createTournament(this.tournament).subscribe(() => {
      this.router.navigate(['/']);
    });
  }
}
