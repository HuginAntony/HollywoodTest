import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddEventComponent } from './add-event/add-event.component';
import { ViewEventsComponent } from './view-events/view-events.component';

const routes: Routes = [
    {
        path: '',
        component: ViewEventsComponent
    },
    {
        path: ':id',
        component: AddEventComponent
    },
    {
        path: 'add',
        component: AddEventComponent
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class EventRoutingModule { }
