import React, {Component} from 'react'
import {Table} from 'react-bootstrap'
import {Button, ButtonToolbar} from 'react-bootstrap'
import {AddDepartament} from './AddDepartament'
import {EditDepartment} from './EditDepartment'

export class Department extends Component {
    constructor(props) {
        super(props);
        this.state = {
            deps: [],
            addModalShow: false,
            editModalShow: false
        }
    }

    refreshList() {
        fetch(process.env.REACT_APP_API + 'department').then(response => response.json()).then(data => {
            this.setState({deps: data})
        })
    }

    componentDidMount() {
        this.refreshList();
    }

    componentDidUpdate() {
        this.refreshList();
    }

    deleteDepartment(depid) {
        if (window.confirm('Are you sure')) {
            fetch(process.env.REACT_APP_API + 'department/' + depid, {
                method: 'DELETE',
                header: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            })
        }
    }
    render() {
        const {deps, depid, depname} = this.state;

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
                        deps.map(_dep => <tr key={
                            _dep.DepartmentId
                        }>
                            <td>{
                                _dep.DepartmentId
                            }</td>
                            <td>{
                                _dep.DepartmentName
                            }</td>
                            <td>
                                <ButtonToolbar>
                                    <Button variant="info"
                                        onClick={
                                            () => this.setState({editModalShow: true, depid: _dep.DepartmentId, depname: _dep.DepartmentName})
                                    }>
                                        Edit
                                    </Button>
                                    &nbsp; &nbsp;
                                    <Button variant="danger"
                                        onClick={
                                            () => this.deleteDepartment(_dep.DepartmentId)
                                    }>
                                        Remove
                                    </Button>

                                    <EditDepartment show={
                                            this.state.editModalShow
                                        }
                                        onHide={editModalClose}
                                        depid={depid}
                                        depname={depname}/>
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
                        Add Department
                    </Button>
                    <AddDepartament show={
                            this.state.addModalShow
                        }
                        onHide={addModalClose}/>

                </ButtonToolbar>
            </div>
        )
    }
}
