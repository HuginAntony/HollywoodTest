import { ViewEventDetailsComponent } from './view-event-details/view-event-details.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddEventDetailsComponent } from './add-event-details/add-event-details.component';
import { ViewEventDetailComponent } from './view-event-detail/view-event-detail.component';

const routes: Routes = [
    {
        path: '',
        component: ViewEventDetailsComponent
    },
    {
        path: ':id',
        component: ViewEventDetailComponent
    },
    {
        path: ':id/edit',
        component: AddEventDetailsComponent
    },
    {
        path: 'add',
        component: AddEventDetailsComponent
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class EventDetailsRoutingModule { }