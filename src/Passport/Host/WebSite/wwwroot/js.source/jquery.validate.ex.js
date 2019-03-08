; (function ($) {
    if (!$) return;
    if (!$.validator) return;

    // 重写 jquery.validate 的 checkForm方法，让每次只验证一条
    $.validator.prototype.checkForm = function () {
        this.prepareForm();
        for (var i = 0, elements = (this.currentElements = this.elements()); elements[i]; i++) {
            if (!this.settings.showAllError) {
                if (!this.check(elements[i])) {
                    break;
                }
            } else {
                this.check(elements[i])
            }
        }
        return this.valid();
    };
    // http://jqueryvalidation.org/Validator.element/
    // 重写单个元素验证
    $.validator.prototype.element = function (element) {
        var cleanElement = this.clean(element),
            checkElement = this.validationTargetFor(cleanElement),
            v = this,
            result = true,
            rs, group;

        if (checkElement === undefined) {
            delete this.invalid[cleanElement.name];
        } else {
            if (this.settings.showAllError) {
                this.prepareElement(checkElement);
            } else {
                // 先隐藏所有错误内容
                this.prepareForm();
            }
            this.currentElements = $(checkElement);

            // If this element is grouped, then validate all group elements already
            // containing a value
            group = this.groups[checkElement.name];
            if (group) {
                $.each(this.groups, function (name, testgroup) {
                    if (testgroup === group && name !== checkElement.name) {
                        cleanElement = v.validationTargetFor(v.clean(v.findByName(name)));
                        if (cleanElement && cleanElement.name in v.invalid) {
                            v.currentElements.push(cleanElement);
                            result = v.check(cleanElement) && result;
                        }
                    }
                });
            }

            rs = this.check(checkElement) !== false;
            result = result && rs;
            if (rs) {
                this.invalid[checkElement.name] = false;
            } else {
                this.invalid[checkElement.name] = true;
            }

            if (!this.numberOfInvalids()) {

                // Hide error containers on last error
                this.toHide = this.toHide.add(this.containers);
            }

            this.showErrors();

            // Add aria-invalid status for screen readers
            $(element).attr("aria-invalid", !rs);
        }

        return result;
    };

    // 添加自定义校验
    $.validator.addMethod("phone", function (value, element, param) {
        return this.optional(element) || /^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$/.test(value);
    }, "Enter a valid 10 digit phone number as XXX-XXX-XXXX.");
    $.validator.addMethod("zipcode", function (value, element, param) {
        return this.optional(element) || /^\d{5}$/.test(value);
    }, "The zip/postal code should be limited in five digit.");
    $.validator.addMethod("cvv", function (value, element, param) {
        return this.optional(element) || /^\d{3,4}$/.test(value);
    }, "The CVV should be limited in 3 or 4 digit.");
    $.validator.addMethod("creditcard", function (value, element) {
        value = value.replace(/\s+/g, "");
        return this.optional(element) || value.length <= 20;
    }, "Please enter no more than 20 characters.");


})(jQuery);