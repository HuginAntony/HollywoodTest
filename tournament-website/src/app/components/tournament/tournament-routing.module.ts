import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Roles } from 'src/app/shared/enums/roles.enum';
import { AuthGuard } from 'src/app/shared/guards/auth.guard';
import { RoleGuard } from 'src/app/shared/guards/role.guard';
import { AddTournamentComponent } from './add-tournament/add-tournament.component';
import { ViewTournamentsComponent } from './view-tournaments/view-tournaments.component';

const routes: Routes = [
  {
    path: '',
    component: ViewTournamentsComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: [Roles.Admin, Roles.User] }
  },
  {
    path: 'add',
    component: AddTournamentComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: [Roles.Admin] }
  },
  {
    path: ':id/edit',
    component: AddTournamentComponent,
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: [Roles.Admin] }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TournamentRoutingModule {}
