import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';
import { AddEventDetailsComponent } from './add-event-details/add-event-details.component';
import { EventDetailsRoutingModule } from './event-details-routing.module';
import { ViewEventDetailComponent } from './view-event-detail/view-event-detail.component';
import { ViewEventDetailsComponent } from './view-event-details/view-event-details.component';

@NgModule({
    declarations: [AddEventDetailsComponent, ViewEventDetailComponent, ViewEventDetailsComponent],
    imports: [CommonModule, SharedModule, EventDetailsRoutingModule, RouterModule],
  })
  export class EventDetailModule {}
