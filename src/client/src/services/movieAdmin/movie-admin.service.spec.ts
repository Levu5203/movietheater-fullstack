import { TestBed } from '@angular/core/testing';

import { MovieAdminService } from './movie-admin.service';

describe('MovieAdminService', () => {
  let service: MovieAdminService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MovieAdminService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
