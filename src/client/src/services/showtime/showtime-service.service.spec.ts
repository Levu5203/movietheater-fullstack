import { TestBed } from '@angular/core/testing';

import { ShowtimeServiceService } from './showtime-service.service';

describe('ShowtimeServiceService', () => {
  let service: ShowtimeServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ShowtimeServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
