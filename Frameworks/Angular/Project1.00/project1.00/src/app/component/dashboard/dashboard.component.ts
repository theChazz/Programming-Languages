import { Component, OnInit } from '@angular/core';
/** Learnt how to use angular forms and service from https://angular.io/start/start-forms 
 * and https://www.youtube.com/watch?v=hvzaUfemy2E&ab_channel=CodeLogic */
import { FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { Employee } from 'src/app/model/employee';
import { EmployeeService } from '../../service/employee.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  employeeDetails !: FormGroup;
  employeeObject : Employee = new Employee();
  employeeList : Employee[] = [];

  constructor(
    private employeeService: EmployeeService,
    private formBuilder: FormBuilder,
  ) {}

  // Create: add new employee
  addNewEmployee(): void {
    // Checks if employee details are been recieved from the front-end form
    console.warn('Your employee details have been submitted', this.employeeDetails.value);
    
    /** use of model. Since the db is set on auto increment no need for employeeID */
    this.employeeObject.EmployeeID = this.employeeDetails.value.employeeID;
    this.employeeObject.Email = this.employeeDetails.value.employeeEmail;
    this.employeeObject.Salary = this.employeeDetails.value.employeeSalary;

    this.employeeService.addEmployee(this.employeeObject).subscribe(
      res=>{console.log(res); this.getAllEmployee();},
      err=>{console.log(err);}
    )
    
    this.employeeDetails.reset();
  }

  // Read
  getAllEmployee(){
    this.employeeService.getEmployees().subscribe(
      res=>{this.employeeList = res;},
      err=>{console.log("error fetching data");}
    )
  }

  // Update
  setEmployeeDetails(employeeModel: Employee){

    this.employeeDetails.controls["employeeID"].setValue(employeeModel.EmployeeID);
    this.employeeDetails.controls["employeeEmail"].setValue(employeeModel.Email);
    this.employeeDetails.controls["employeeSalary"].setValue(employeeModel.Salary);
  }

  updateSelectedEmployee(): void {
    // Checks if employee details are been recieved from the front-end form
    console.warn('Your employee details have been submitted', this.employeeDetails.value);
      
    /** use of model. Since the db is set on auto increment no need for employeeID */
    this.employeeObject.EmployeeID = this.employeeDetails.value.employeeID;
    this.employeeObject.Email = this.employeeDetails.value.employeeEmail;
    this.employeeObject.Salary = this.employeeDetails.value.employeeSalary;

    this.employeeService.updateEmployee(this.employeeObject).subscribe(
    res=>{console.log(res); this.getAllEmployee();},
    err=>{console.log(err);
  })

  }
    
  // Delete
  deleteSelectedEmployee(employeeModel: Employee): void{
    this.employeeService.deleteEmployee(employeeModel).subscribe(
      res=>{console.log(res);
      alert("Employee record deleted");
      this.getAllEmployee();
    },
    err=>{console.log(err);
    });
  }

  ngOnInit(): void {
      
      this.getAllEmployee();

      this.employeeDetails  = this.formBuilder.group({
      employeeID: [''],
      employeeEmail: [''],
      employeeSalary: ['']
    });
  }

}
