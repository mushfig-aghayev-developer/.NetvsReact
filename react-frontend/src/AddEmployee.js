import React,{Component} from 'react';
import {Modal,Button, Row, Col, Form} from 'react-bootstrap';

export class AddEmployee extends Component{
    constructor(props){
        super(props);
        this.handleSubmit=this.handleSubmit.bind(this);
    }

    handleSubmit(event){
        event.preventDefault();
        if(event.target.value !== ''){
        fetch(process.env.REACT_APP_API + 'employee',{
            method:'POST',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json'
            },
            body:JSON.stringify({
                EmployeeID:'0',
                EmoployeeName:event.target.EmoployeeName.value
            })
        })
        .then(res=>res.json())
        .then((result)=>{
           // alert(result);
           event.target.DepartmentName.value = ''
        },
        (error)=>{
            alert('Failed');
        })
    }
    else
    alert('Required');
    }
    render(){
        return (
            <div className="container">

<Modal
{...this.props}
size="lg"
aria-labelledby="contained-modal-title-vcenter"
centered>

    <Modal.Header closeButton>
        <Modal.Title id="contained-modal-title-vcenter">
            Add Employee
        </Modal.Title>
    </Modal.Header>
    <Modal.Body>
        <Row>
            <Col sm={6}>
                <Form onSubmit={this.handleSubmit}>
                    <Form.Group controlId="EmoployeeName">
                        <Form.Label>EmoployeeName</Form.Label>
                        <Form.Control type="text" name="EmoployeeName" required 
                        placeholder="EmoployeeName"/>
                    </Form.Group>

                    <Form.Group className="mt-3">
                        <Button variant="primary" type="submit">
                            Add Emoployee
                        </Button>
                    </Form.Group>
                </Form>
            </Col>
        </Row>
    </Modal.Body>
    
      <Modal.Footer>
        <Button variant="danger" onClick={this.props.onHide}>Close</Button>
      </Modal.Footer>
     </Modal>

            </div>
        )
    }

}