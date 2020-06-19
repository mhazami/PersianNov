
(function () {

    var supportAnimations = 'WebkitAnimation' in document.body.style ||
        'MozAnimation' in document.body.style ||
        'msAnimation' in document.body.style ||
        'OAnimation' in document.body.style ||
        'animation' in document.body.style,
        animEndEventNames = {
            'WebkitAnimation': 'webkitAnimationEnd',
            'OAnimation': 'oAnimationEnd',
            'msAnimation': 'MSAnimationEnd',
            'animation': 'animationend'
        },
        // animation end event name
        animEndEventName = animEndEventNames[Modernizr.prefixed('animation')],
        scrollWrap = document.getElementById('scroll-wrap'),
        docscroll = 0,
        books = document.querySelectorAll('#bookshelf > figure');

    function scrollY() {
        return window.pageYOffset || window.document.documentElement.scrollTop;
    }

    function Book(el) {
        this.el = el;
        this.book = this.el.querySelector('.book');
        this.ctrls = this.el.querySelector('.buttons');
        this.details = this.el.querySelector('.details');
        // create the necessary structure for the books to rotate in 3d
        this._layout();

        this.bbWrapper = document.getElementById(this.book.getAttribute('data-book'));
        if (this.bbWrapper) {
            this._initBookBlock();
        }
        this._initEvents();
    }

    Book.prototype._layout = function () {
        if (Modernizr.csstransforms3d) {
            //this.book.innerHTML = '<div class="cover"><div class="front"></div><div class="inner inner-left"></div></div><div class="inner inner-right"></div>';
            //var perspective = document.createElement('div');
            //perspective.className = 'perspective';
            //perspective.appendChild(this.book);
            //this.el.insertBefore(perspective, this.ctrls);
        }
        if (this.details) {
            this.closeDetailsCtrl = document.createElement('span')
            this.closeDetailsCtrl.className = 'close-details';
            this.details.appendChild(this.closeDetailsCtrl);
        }
    }

    Book.prototype._initBookBlock = function () {
        // initialize bookblock instance
        this.bb = new BookBlock(this.bbWrapper.querySelector('.bb-bookblock'), {
            speed: 700,
            shadowSides: 0.8,
            shadowFlip: 0.4
        });
        // boobkblock controls
        this.ctrlBBClose = this.bbWrapper.querySelector(' .bb-nav-close');
        this.ctrlBBNext = this.bbWrapper.querySelector(' .bb-nav-next');
        this.ctrlBBPrev = this.bbWrapper.querySelector(' .bb-nav-prev');
    }

    Book.prototype._initEvents = function () {
        var self = this;
        if (!this.ctrls) return;

        //if (this.bb) {
        //    this.ctrls.querySelector('a:nth-child(1)').addEventListener('click', function (ev) { /*ev.preventDefault();*/ self._open(); });
        //    this.ctrlBBClose.addEventListener('click', function (ev) { ev.preventDefault(); self._close(); });
        //    this.ctrlBBNext.addEventListener('click', function (ev) {
        //        ev.preventDefault();
        //        console.log('next called')
        //        self._nextPage();
        //    });
        //    this.ctrlBBPrev.addEventListener('click', function (ev) { ev.preventDefault(); self._prevPage(); });
        //}

        if (this.details) {
            this.ctrls.querySelector('a:nth-child(2)').addEventListener('click', function (ev) { ev.preventDefault(); self._showDetails(); });
            this.closeDetailsCtrl.addEventListener('click', function () { self._hideDetails(); });
        }
    }

    Book.prototype._open = function () {
        //docscroll = scrollY();
        //var id = this.el.getAttribute('data-id');
        //var index = this.el.getAttribute('data-index');
        //getParts(id, index);

        //console.log(this.el);
        //classie.add(this.el, 'open');
        //classie.add(this.bbWrapper, 'show');

        //var self = this,
        //    onOpenBookEndFn = function (ev) {
        //        this.removeEventListener(animEndEventName, onOpenBookEndFn);
        //        document.body.scrollTop = document.documentElement.scrollTop = 0;
        //        classie.add(scrollWrap, 'hide-overflow');
        //    };

        //if (supportAnimations) {
        //    this.bbWrapper.addEventListener(animEndEventName, onOpenBookEndFn);
        //}
        //else {
        //    onOpenBookEndFn.call();
        //}

    }

    //Book.prototype._close = function () {
    //    classie.remove(scrollWrap, 'hide-overflow');
    //    setTimeout(function () { document.body.scrollTop = document.documentElement.scrollTop = docscroll; }, 25);
    //    classie.remove(this.el, 'open');
    //    classie.add(this.el, 'close');
    //    classie.remove(this.bbWrapper, 'show');
    //    classie.add(this.bbWrapper, 'hide');

    //    var self = this,
    //        onCloseBookEndFn = function (ev) {
    //            this.removeEventListener(animEndEventName, onCloseBookEndFn);
    //            // reset bookblock starting page
    //            self.bb.jump(1);
    //            classie.remove(self.el, 'close');
    //            classie.remove(self.bbWrapper, 'hide');
    //        };

    //    if (supportAnimations) {
    //        this.bbWrapper.addEventListener(animEndEventName, onCloseBookEndFn);
    //    }
    //    else {
    //        onCloseBookEndFn.call();
    //    }
    //}

    //Book.prototype._nextPage = function () {
    //    this.bb.next();
    //}

    //Book.prototype._prevPage = function () {
    //    this.bb.prev();
    //}

    Book.prototype._showDetails = function () {
        classie.add(this.el, 'details-open');
    }

    Book.prototype._hideDetails = function () {
        classie.remove(this.el, 'details-open');
    }

    function init() {
        [].slice.call(books).forEach(function (el) {
            new Book(el);
        });
    }

    init();

    async function getParts(id, index) {
        console.log(id);
        var target = document.getElementById('parts');
        const response = await fetch('/BookPart/GetBookParts?id=' + id, {
            method: 'GET',
            cache: 'no-cache',
            credentials: 'same-origin',
            headers: {
                'Content-Type': 'application/json'
            },

        });
            //var form = '<div class="bb-custom-wrapper" id="book-1">' +
            //    '<div class="bb-bookblock">' +
            //    '<div class="bb-item" >' +
            //    '<div class="bb-custom-side page-layout-1">' +
            //    '<div>' +
            //    '<h3>1</h3>' +
            //    '<p>' +

            //    '</p>' +
            //    '</div>' +
            //    '</div>' +
            //    '<div class="bb-custom-side page-layout-1">' +
            //    '<h3>' +

            //    ' </h3>' +
            //    '</div>' +
            //    '</div >' +
            //    '</div >' +
            //    '<nav>' +
            //    '<a href="#" class="bb-nav-prev">Previous</a>' +
            //    '<a href="#" class="bb-nav-next">Next</a>' +
            //    '<a href="#" class="bb-nav-close">Close</a>' +
            //    '</nav>' +
            //    '</div >';
            ////    for (var i = 0; i < response.length; i++) {

            ////}
            //target.append(form);
          

    }

})();

