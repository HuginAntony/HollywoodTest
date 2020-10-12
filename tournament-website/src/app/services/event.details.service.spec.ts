/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { EventDetailsService } from './event.details.service';

describe('Service: Event.details.service', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [EventDetailsService]
    });
  });

  it('should ...', inject([EventDetailsService], (service: EventDetailsService) => {
    expect(service).toBeTruthy();
  }));
});
