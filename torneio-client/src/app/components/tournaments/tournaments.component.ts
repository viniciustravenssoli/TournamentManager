import { Component } from '@angular/core';
import { Tournament, TournamentToList } from 'src/app/Models/models';
import { TournamentService } from 'src/app/services/tournament.service';

@Component({
  selector: 'app-tournaments',
  templateUrl: './tournaments.component.html',
  styleUrls: ['./tournaments.component.css']
})
export class TournamentsComponent {
  tournaments: TournamentToList[] = [];

  constructor(private tournamentService: TournamentService) {}

  ngOnInit(): void {
    this.tournamentService.getAllTournaments().subscribe((data: TournamentToList[]) => {
      this.tournaments = data;
    });
  }
}
