import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TournamentsComponent } from './components/tournaments/tournaments.component';
import { TournamentDetailComponent } from './components/tournament-detail/tournament-detail.component';
import { CreateTournamentComponent } from './components/create-tournament/create-tournament.component';

const routes: Routes = [
  { path: '', component: TournamentsComponent },
  { path: 'tournament/:id', component: TournamentDetailComponent },
  { path: 'create-tournament', component: CreateTournamentComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
