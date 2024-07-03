import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Match, Tournament } from 'src/app/Models/models';
import { TournamentService } from 'src/app/services/tournament.service';



@Component({
  selector: 'app-tournament-detail',
  templateUrl: './tournament-detail.component.html',
  styleUrls: ['./tournament-detail.component.css']
})
export class TournamentDetailComponent {
  tournament: Tournament | undefined;
  matchesGroupedByRound: { [key: number]: Match[] } = {};
  selectedWinnerId: number | undefined;

  constructor(
    private route: ActivatedRoute,
    private tournamentService: TournamentService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.tournamentService.getTournamentById(id).subscribe((data: Tournament) => {
      this.tournament = data;
      this.groupMatchesByRound();
    });
  }

  groupMatchesByRound(): void {
    if (this.tournament) {
      this.matchesGroupedByRound = this.tournament.matches.reduce((acc: { [key: number]: Match[] }, match: Match) => {
        if (!acc[match.round]) {
          acc[match.round] = [];
        }
        acc[match.round].push(match);
        return acc;
      }, {});
    }
  }

  setWinner(matchId: number, winnerId: number | undefined): void {
    if (winnerId !== undefined) {
      this.tournamentService.setMatchWinner(matchId, winnerId).subscribe(() => {
        this.ngOnInit(); // Refresh the data after setting the winner
      });
    }
  }
}
