import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { AddEventComponent } from './add-event/add-event.component';
import { EventRoutingModule } from './event-routing.module';
import { ViewEventsComponent } from './view-events/view-events.component';

@NgModule({
    declarations: [AddEventComponent, ViewEventsComponent],
    imports: [CommonModule, SharedModule, EventRoutingModule, RouterModule],
  })
  export class EventModule {}
