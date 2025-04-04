import { HttpClient } from '@angular/common/http';
import { CustomerService } from './customer.service';

describe('CustomerService', () => {
  it('should create an instance', () => {
    const mockHttpClient = {} as HttpClient;
    expect(new CustomerService(mockHttpClient)).toBeTruthy();
  });
});
