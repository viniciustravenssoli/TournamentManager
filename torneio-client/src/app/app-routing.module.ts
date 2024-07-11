import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TournamentsComponent } from './components/tournaments/tournaments.component';
import { TournamentDetailComponent } from './components/tournament-detail/tournament-detail.component';
import { CreateTournamentComponent } from './components/create-tournament/create-tournament.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { AuthGuard } from './services/AuthGuard';

const routes: Routes = [
  { path: '', redirectTo: 'tournaments', pathMatch: 'full' },
  { path: '', component: TournamentsComponent, canActivate: [AuthGuard] },
  { path: 'tournament/:id', component: TournamentDetailComponent,  canActivate: [AuthGuard] },
  { path: 'create-tournament', component: CreateTournamentComponent,  canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
