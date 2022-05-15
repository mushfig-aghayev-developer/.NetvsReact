import React, {Component} from 'react'
import {Table} from 'react-bootstrap'
import {Button, ButtonToolbar} from 'react-bootstrap'
import {AddEmployee} from './AddEmployee'
import {EditEmployee} from './EditEmployee';

export class Employee extends Component {
    constructor(props) {
        super(props);
        this.state = {
            employees: [],
            addModalShow: false,
            editModalShow: false
        }
    }

    refreshList() {
        fetch(process.env.REACT_APP_API + 'employee')
        .then(response => response.json())
        .then(data => {
            this.setState({employees: data})
        })
    }

    componentDidMount() {
        this.refreshList();
    }

    componentDidUpdate() {
        this.refreshList();
    }

    deleteEmployee(empid) {
        if (window.confirm('Are you sure')) {
            fetch(process.env.REACT_APP_API + 'employee/' + empid, {
                method: 'DELETE',
                header: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            })
        }
    }
    render() {
        const {employees, empid, empname} = this.state;

        let addModalClose = () => this.setState({addModalShow: false})
        let editModalClose = () => this.setState({editModalShow: false})

        return (
            <div className='mb-5'>
                <Table className="mt-4" stripped="true" bordered hover size="sm">
                    <thead>
                        <tr>
                            <th>Department_ID</th>
                            <th>Department_Name</th>
                            <th>Options</th>
                        </tr>
                    </thead>

                    <tbody> {
                        employees.map(_emp => <tr key={
                            _emp.EmployeeID
                        }>
                            <td>{
                                _emp.EmployeeID
                            }</td>
                            <td>{
                                _emp.EmoployeeName
                            }</td>
                            <td>
                                <ButtonToolbar>
                                    <Button variant="info"
                                        onClick={
                                            () => this.setState({editModalShow: true, 
                                                empid:_emp.EmployeeID, empname:_emp.EmoployeeName})}>
                                        Edit
                                    </Button>
                                    &nbsp; &nbsp;
                                    <Button variant="danger"
                                        onClick={
                                            () => this.deleteEmployee(_emp.EmployeeID)
                                    }>
                                        Remove
                                    </Button>

                                    <EditEmployee show={
                                            this.state.editModalShow
                                        }
                                        onHide={editModalClose}
                                        empid={empid}
                                        empname={empname}/>
                                </ButtonToolbar>
                            </td>
                        </tr>)
                    } </tbody>
                </Table>
                <ButtonToolbar>
                    <Button variant='primary'
                        onClick={
                            () => this.setState({addModalShow: true})
                    }>
                        Add Employee
                    </Button>
                    <AddEmployee show={
                            this.state.addModalShow
                        }
                        onHide={addModalClose}/>
                </ButtonToolbar>
            </div>
        )
    }
}
