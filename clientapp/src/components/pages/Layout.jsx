import React from 'react';
import { Outlet } from 'react-router-dom';
import { Navbar, Container, Nav, NavDropdown } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import { Footer } from './Footer';

const Layout = () => {
    return (
        <>
            <header>
                <Navbar collapseOnSelect expand="lg" bg="dark" variant="dark" className='justify-content-center'>
                    <Container>
                        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                        <Navbar.Brand href="/">Home</Navbar.Brand>
                        <Navbar.Collapse id="responsive-navbar-nav">
                            <Nav className="me-auto">
                                <LinkContainer to={"/"}>
                                    <Nav.Link>Home</Nav.Link>
                                </LinkContainer>
                                <LinkContainer to={"/files"}>
                                    <Nav.Link>Files</Nav.Link>
                                </LinkContainer>
                                {/* <NavLink to="/">Home</NavLink>
                                <NavLink to="/files">Files</NavLink> */}
                                <NavDropdown title="Dropdown" id="collasible-nav-dropdown">
                                    <LinkContainer to={"/files/form"}>
                                        <NavDropdown.Item>Form</NavDropdown.Item>
                                    </LinkContainer>
                                    <NavDropdown.Divider />
                                    <NavDropdown.Item href="4">Separated link</NavDropdown.Item>
                                </NavDropdown>
                            </Nav>
                        </Navbar.Collapse>
                    </Container>
                </Navbar>
            </header>
            <main>
                <Container>
                    <Outlet />
                </Container>
            </main>
            <Footer />
        </>
    );
}

export { Layout };
