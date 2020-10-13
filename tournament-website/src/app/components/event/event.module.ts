import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';
import { AddEventComponent } from './add-event/add-event.component';
import { EventRoutingModule } from './event-routing.module';
import { ViewEventComponent } from './view-event/view-event.component';
import { ViewEventsComponent } from './view-events/view-events.component';

@NgModule({
    declarations: [AddEventComponent, ViewEventComponent, ViewEventsComponent],
    imports: [CommonModule, SharedModule, EventRoutingModule, RouterModule],
  })
  export class EventModule {}
