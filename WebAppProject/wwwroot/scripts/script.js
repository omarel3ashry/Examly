// Selecting all elements with class 'sel'
document.querySelectorAll('.sel').forEach(function(selElement) {
    // Hiding the child select elements
    selElement.querySelector('select').style.display = 'none';
    
    var current = selElement;
    
    // Iterating over each option element
    selElement.querySelectorAll('option').forEach(function(option, i) {
        if (i === 0) {
            // Creating and prepending a div element with class 'sel__box'
            var div = document.createElement('div');
            div.className = current.className.replace(/sel/g, 'sel__box');
            current.insertBefore(div, current.firstChild);
            
            var placeholder = option.textContent;
            
            // Creating and prepending a span element with class 'sel__placeholder'
            var span = document.createElement('span');
            span.className = current.className.replace(/sel/g, 'sel__placeholder');
            span.textContent = placeholder;
            span.setAttribute('data-placeholder', placeholder);
            current.insertBefore(span, current.firstChild);
            
            return;
        }
        
        // Appending span elements with class 'sel__box__options' to the previously created div
        var span = document.createElement('span');
        span.className = current.className.replace(/sel/g, 'sel__box__options');
        span.textContent = option.textContent;
        current.querySelector('.sel__box').appendChild(span);
    });
});

// Adding click event listener to elements with class 'sel' to toggle the 'active' class
document.querySelectorAll('.sel').forEach(function(selElement) {
    selElement.addEventListener('click', function() {
        selElement.classList.toggle('active');
    });
});

// Adding click event listener to elements with class 'sel__box__options' to handle selection
document.querySelectorAll('.sel__box__options').forEach(function(optionElement) {
    optionElement.addEventListener('click', function() {
        var txt = optionElement.textContent;
        var index = Array.from(optionElement.parentNode.children).indexOf(optionElement);
        
        // Removing 'selected' class from sibling options and adding it to the clicked option
        optionElement.parentNode.querySelectorAll('.sel__box__options').forEach(function(option) {
            option.classList.remove('selected');
        });
        optionElement.classList.add('selected');
        
        var currentSel = optionElement.closest('.sel');
        
        // Setting the text of the placeholder span to the selected option text
        currentSel.querySelector('.sel__placeholder').textContent = txt;
        
        // Setting the selectedIndex of the corresponding select element
        currentSel.querySelector('select').selectedIndex = index + 1;
    });
});