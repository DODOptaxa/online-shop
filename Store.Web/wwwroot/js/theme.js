document.addEventListener('DOMContentLoaded', () => {
    const toggleButton = document.getElementById('theme-toggle');
    const htmlElement = document.documentElement;

    // Встановлюємо текст кнопки на основі поточної теми
    const currentTheme = htmlElement.getAttribute('data-theme');
    toggleButton.textContent = currentTheme === 'light' ? 'Темна тема' : 'Світла тема';

    toggleButton.addEventListener('click', () => {
        const currentTheme = htmlElement.getAttribute('data-theme');
        const newTheme = currentTheme === 'light' ? 'dark' : 'light';

        htmlElement.setAttribute('data-theme', newTheme);
        localStorage.setItem('theme', newTheme);
        toggleButton.textContent = newTheme === 'light' ? 'Темна тема' : 'Світла тема';
    });
});