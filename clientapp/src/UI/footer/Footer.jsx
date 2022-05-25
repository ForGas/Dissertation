import React from "react"
import { Container} from 'react-bootstrap';
import './footer.css'

export const Footer = () =>
    <footer className="page-footer font-small blue pt-4">
        <section className="footer_wrapper">
            <Container>
                <div className="footer-copyright text-center py-3">
                    <p className="footer_text">© 2022 Copyright</p>
                </div>
            </Container>
        </section>
    </footer>