import { ViewEventDetailsComponent } from './view-event-details/view-event-details.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddEventDetailsComponent } from './add-event-details/add-event-details.component';
import { ViewEventDetailComponent } from './view-event-detail/view-event-detail.component';
import { AuthGuard } from 'src/app/shared/guards/auth.guard';
import { RoleGuard } from 'src/app/shared/guards/role.guard';
import { Roles } from 'src/app/shared/enums/roles.enum';

const routes: Routes = [
    {
        path: '',
        component: ViewEventDetailsComponent,
        canActivate: [AuthGuard, RoleGuard],
        data: { roles: [Roles.Admin, Roles.User] }
    },
    {
        path: 'add',
        component: AddEventDetailsComponent,
        canActivate: [AuthGuard, RoleGuard],
        data: { roles: [Roles.Admin] }
    },
    {
        path: ':id',
        component: ViewEventDetailComponent,
        canActivate: [AuthGuard, RoleGuard],
        data: { roles: [Roles.Admin, Roles.User] }
    },
    {
        path: ':id/edit',
        component: AddEventDetailsComponent,
        canActivate: [AuthGuard, RoleGuard],
        data: { roles: [Roles.Admin] }
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class EventDetailsRoutingModule { }