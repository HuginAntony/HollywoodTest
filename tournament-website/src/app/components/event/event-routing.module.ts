import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Roles } from 'src/app/shared/enums/roles.enum';
import { AuthGuard } from 'src/app/shared/guards/auth.guard';
import { RoleGuard } from 'src/app/shared/guards/role.guard';
import { AddEventComponent } from './add-event/add-event.component';
import { ViewEventComponent } from './view-event/view-event.component';
import { ViewEventsComponent } from './view-events/view-events.component';

const routes: Routes = [
    {
        path: '',
        component: ViewEventsComponent,
        canActivate: [AuthGuard, RoleGuard],
        data: { roles: [Roles.Admin, Roles.User] }
    },
    {
        path: 'add',
        component: AddEventComponent,
        canActivate: [AuthGuard, RoleGuard],
        data: { roles: [Roles.Admin] }
    },
    {
        path: ':id',
        component: ViewEventComponent,
        canActivate: [AuthGuard, RoleGuard],
        data: { roles: [Roles.Admin, Roles.User] }
    },
    {
        path: ':id/edit',
        component: AddEventComponent,
        canActivate: [AuthGuard, RoleGuard],
        data: { roles: [Roles.Admin] }
    },
   
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class EventRoutingModule { }
