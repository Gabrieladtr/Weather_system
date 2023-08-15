function fadeInOnScroll() {
    const sections = document.querySelectorAll('section');
    sections.forEach((section) => {
        const sectionTop = section.getBoundingClientRect().top;
        const windowHeight = window.innerHeight;

        if (sectionTop < windowHeight) {
            section.style.opacity = 1;
            section.style.transform = 'translateY(0)';
        }
    });
}

// Atualiza o efeito de fade-in quando a página é rolada
window.addEventListener('scroll', fadeInOnScroll);