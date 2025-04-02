import { EmployeeModel } from '../../models/user/employee.model';
import { IMasterDataService } from '../master-data/master-data.service.interface';

export interface IEmployeeService extends IMasterDataService<EmployeeModel> {}
