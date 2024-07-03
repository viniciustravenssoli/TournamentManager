import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TournamentsComponent } from './components/tournaments/tournaments.component';
import { TournamentDetailComponent } from './components/tournament-detail/tournament-detail.component';
import { CreateTournamentComponent } from './components/create-tournament/create-tournament.component';

@NgModule({
  declarations: [
    AppComponent,
    TournamentsComponent,
    TournamentDetailComponent,
    CreateTournamentComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
