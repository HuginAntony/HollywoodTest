import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddTournamentComponent } from './add-tournament/add-tournament.component';
import { ViewTournamentsComponent } from './view-tournaments/view-tournaments.component';

const routes: Routes = [
  {
    path: '',
    component: ViewTournamentsComponent
  },
  {
    path: 'add',
    component: AddTournamentComponent
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TournamentRoutingModule {}
