import { ViewTournamentsComponent } from './view-tournaments/view-tournaments.component';

import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { TournamentRoutingModule } from './tournament-routing.module';
import { AddTournamentComponent } from './add-tournament/add-tournament.component';

@NgModule({
    declarations: [AddTournamentComponent,ViewTournamentsComponent],
    imports: [CommonModule, SharedModule, TournamentRoutingModule, RouterModule],
  })
  export class TournamentModule {}
