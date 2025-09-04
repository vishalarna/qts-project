import { ApiEmployeesService } from './api.employees.service';
import { StubEmployeesService } from './stub.employees.service';
export interface IEmployeesService {
  getAll: () => Promise<any>;
}

function clientSettingsServiceFactory() {
  //here you can either inject params in to determine whic service to use OR detect an env var
  return new StubEmployeesService();
}

export const clientSettingsServiceProvider = {
  provide: ApiEmployeesService,
  useFactory: clientSettingsServiceFactory,
};
