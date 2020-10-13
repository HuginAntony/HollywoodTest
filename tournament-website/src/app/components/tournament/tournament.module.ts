import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';
import { TournamentRoutingModule } from './tournament-routing.module';
import { AddTournamentComponent } from './add-tournament/add-tournament.component';
import { ViewTournamentsComponent } from './view-tournaments/view-tournaments.component';

@NgModule({
    declarations: [AddTournamentComponent,ViewTournamentsComponent],
    imports: [CommonModule, SharedModule, TournamentRoutingModule, RouterModule],
  })
  export class TournamentModule {}
