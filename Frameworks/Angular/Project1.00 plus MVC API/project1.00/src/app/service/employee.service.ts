import { Injectable } from '@angular/core';
import { HttpClient, HttpClientModule } from "@angular/common/http";
import { Observable } from 'rxjs';
import { Employee } from '../model/employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  addEmployeeURL : string;
  getEmployeeURL : string;
  updateEmployeeURL : string;
  deleteEmployeeURL : string;

  constructor(private http : HttpClient) { 

    this.addEmployeeURL = "https://localhost:44358/api/Employees";
    this.getEmployeeURL = "https://localhost:44358/api/Employees";
    this.updateEmployeeURL = "https://localhost:44358/api/Employees";
    this.deleteEmployeeURL = "https://localhost:44358/api/Employees";
    
  
  }

  // Create: Service function
  addEmployee(employee : Employee): Observable<Employee> {
    
    return this.http.post<Employee>(this.addEmployeeURL, employee);
    
  }

  // Read: Service function 
  getEmployees(): Observable<Employee[]>{

    return this.http.get<Employee[]>(this.getEmployeeURL);

  }

  // Update: Service function
  updateEmployee(employee : Employee): Observable<Employee> {
  
    return this.http.put<Employee>(this.updateEmployeeURL, employee);
    
  }

  // Delete: Service function
  // Depending on how the API is built determines how angular to call the function, therefore is used ?EmployeeID= just like post man tests each function
  deleteEmployee(employee : Employee): Observable<Employee> {
  
    return this.http.delete<Employee>(this.addEmployeeURL+'?EmployeeID='+employee.EmployeeID);
    
  }

}
